using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using QTF.Data.Abstraction;
using QTF.Domain.Entity.UserBundle;
using QTF.Dtos.UserBundle;
using QTF.RepositoryContract;
using QTF.Service.Exceptions;
using QTF.ServiceContract;
using System.Reflection.Metadata;
using QTF.Dtos;

namespace QTF.Service
{

    /// <summary>
    /// manage the address for user
    /// </summary>
    public class AddressService :BaseService, IAddressService
    {
        //TODO: this whole class needs to be inherited from generic service too, but i already done too much to get this job
        private readonly IUnitOfWork _db;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserService _userService;
        IMapper _mapper;
        public AddressService(IUnitOfWork unitOfWork, IAddressRepository addressRepository, IMapper mapper, IUserService userService)
        {

            _db = unitOfWork;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _userService = userService;
        }
        public IEnumerable<GetAddressDto> GetAll()
        {

            var result = _addressRepository.GetAll();

            return _mapper.Map<List<GetAddressDto>>(result);
        }


        public ValidationInformation Add(AddressDto item)
        {
            var validationResult = ManualValidator(item);
            if (_userService.CheckUserKey(item.UserKey) && validationResult.Success)//this template is repeated , we could change it to more elegant way but for demo its ok
            {
                var entity = _mapper.Map<AddressEntity>(item);

                _addressRepository.Add(entity);
                _db.SaveChanges();
                item = _mapper.Map<AddressDto>(entity);
                validationResult.Entity = item;
            }


            return validationResult;
        }


        public ValidationInformation Edit(AddressDto item)
        {
            var validationResult = ManualValidator(item);
            if (_userService.CheckUserKey(item.UserKey) && validationResult.Success)//this template is repeated , we could change it to more elegant way but for demo its ok
            {
                var entity = _mapper.Map<AddressEntity>(item);

                _addressRepository.Update(entity, entity.Key);
                SaveChanges();
                item = _mapper.Map<AddressDto>(entity);
                validationResult.Entity = item;
            }


            return validationResult;
        }

        public void Remove(AddressDto entity)
        {

            Remove(entity.Key);


        }
        public void Remove(int key)
        {
            var entity = _addressRepository.Get(key);
            _addressRepository.Delete(entity);
            SaveChanges();

        }




        public AddressDto Get(int key)
        {
            var entity = _addressRepository.Get(key);
            var item = _mapper.Map<AddressDto>(entity);
            return item;
        }



        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }


    }
}
