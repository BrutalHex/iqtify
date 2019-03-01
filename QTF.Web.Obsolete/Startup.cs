using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QTF.Data.Models;
using QTF.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using QTF.Data;
using QTF.Data.Abstraction;
using QTF.Data.Infra;
using QTF.Domain.Entity.UserBundle;

namespace QTF.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private ServiceProvider RegisterExtraServices(IServiceCollection services)
        {
            /*
             * this is not good way !!!!!!!!!!!!!!!!!!!
             * 
             */
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //registering repository  
            var repoDi = new QTF.Repository.Infra.RegisterRepositories(services);
            //registering services
            var repoService = new QTF.Service.Infra.RegisterServices(services);

            return services.BuildServiceProvider();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QtfDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<QtfDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddAuthentication().AddOAuth("GitHubAuth", "Log in with GitHub", options =>
            {
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.CallbackPath = new PathString("/signin-github");
                options.ClaimsIssuer = "OAuth2-Github";
                options.ClientId = Configuration["GitHub:Client_ID"];
                options.ClientSecret = Configuration["GitHub:Client_Secret"];
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";
                options.SaveTokens = true;

                options.Scope.Add("user:email");

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context => { await CreatingGitHubAuthTicket(context); }
                };
            });

            services.AddScoped<IQuestionService, QuestionService>();

            var sp = RegisterExtraServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private static async Task CreatingGitHubAuthTicket(OAuthCreatingTicketContext context)
        {
            //TODO: Refactor to avoid code duplications
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var user = JObject.Parse(await response.Content.ReadAsStringAsync());

            request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint + "/emails");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var emails = JArray.Parse(await response.Content.ReadAsStringAsync());
            
            AddClaims(context, user, emails);
        }

        private static void AddClaims(OAuthCreatingTicketContext context, JObject user, JArray emails)
        {
            var userName = user.Value<string>("login");
            if (!string.IsNullOrEmpty(userName))
            {
                context.Identity.AddClaim(new Claim(
                    ClaimsIdentity.DefaultNameClaimType, userName,
                    ClaimValueTypes.String, context.Options.ClaimsIssuer));
            }

            foreach (JObject email in emails.Children<JObject>())
            {
                bool isPrimary = email.Value<bool>("primary");
                if (isPrimary)
                {
                    var emailVale = email.Value<string>("email");
                    context.Identity.AddClaim(new Claim(
                        ClaimTypes.Email, emailVale,
                        ClaimValueTypes.Email, context.Options.ClaimsIssuer));
                }
            }

            var identifier = user.Value<string>("id");
            if (!string.IsNullOrEmpty(identifier))
            {
                context.Identity.AddClaim(new Claim(
                    ClaimTypes.NameIdentifier, identifier,
                    ClaimValueTypes.String, context.Options.ClaimsIssuer));
            }

            var name = user.Value<string>("name");
            if (!string.IsNullOrEmpty(name))
            {
                context.Identity.AddClaim(new Claim(
                    "urn:github:name", name,
                    ClaimValueTypes.String, context.Options.ClaimsIssuer));
            }

            var link = user.Value<string>("url");
            if (!string.IsNullOrEmpty(link))
            {
                context.Identity.AddClaim(new Claim(
                    "urn:github:url", link,
                    ClaimValueTypes.String, context.Options.ClaimsIssuer));
            }
        }
    }
}
