using AccessLDAP.Common;
using AccessLDAP.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLDAP.BL
{
    public static class AutoMapperConfiguration
    {
        public static  IMapper configurationTOEntity;
        public static  IMapper configurationTODto;
        public static void Configure()
        {
            ConfigureUserToDTOMapping();
            ConfigureDTOToUserMapping();
         }

        private static void ConfigureUserToDTOMapping()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            configurationTOEntity = config.CreateMapper();
        }

        private static void ConfigureDTOToUserMapping()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>());
            configurationTODto = config.CreateMapper();
        }
    }
}
