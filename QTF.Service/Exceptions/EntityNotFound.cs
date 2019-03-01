using System;
using System.Collections.Generic;
using System.Text;

namespace QTF.Service.Exceptions
{
    //this one lacks of ISerializable 
    public class EntityNotFound:Exception
    {
        public EntityNotFound(string entityName,object key):base(string.Format(Resource.Messages.EntityNotFound, entityName, key))
        {

        }
    }
}
