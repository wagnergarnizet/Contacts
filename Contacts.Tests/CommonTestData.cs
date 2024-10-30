using Fiap.Team10.Contacts.Domain.DTOs.Application;
using Fiap.Team10.Contacts.Domain.DTOs.EntityDTOs;
using Fiap.Team10.Contacts.Domain.Entities;

namespace Fiap.Team10.Contacts.Tests
{
    public static class CommonTestData
    {
        public static ContactCreateDto GetInsertDtoObject()
           => new()
           {
               Name = "Marcelo Cedro",
               AreaCode = "11",
               Phone = "982840611",
               Email = "marceloced@gmail.com"
           };
        public static ContactUpdateDto GetUpdateDtoObject()
        => new()
        {
            Id = 1,
            Name = "Marcelo Cedro",
            AreaCode = "11",
            Phone = "982840611",
            Email = "marceloced@gmail.com"
        };

        public static UpsertContactResponse GetResponseUpsertObject(bool success, bool insert)
        {
            var operationSuccess = insert ? "inserido" : "alterado";
            var operationFail = insert ? "inserir" : "alterar";
            string message = success ?
                   $"Contato {operationSuccess} com sucesso." :
                   $"Ocorreu um problema ao tentar {operationFail} o registro. Erro: ERRO-SIMULADO";

            return new()
            {
                Success = success,
                Message = message
            };
        }

        public static Contact GetContactObject() 
            => new()
            {
                Id = 1,
                Name = "Wagner Garnizet",
                AreaCode = "11",
                Phone = "982878151",
                Email = "wagnergarniz@gmail.com"
            };

        public static List<Contact> GetContacListObject()
            =>
            [
                new ()
                {
                    Id = 1,
                    Name = "Wagner Garnizet",
                    AreaCode = "11",
                    Phone = "982878151",
                    Email = "wagnergarniz@gmail.com"
                },
                new ()
                {
                    Id = 1,
                    Name = "Marcelo Cedro",
                    AreaCode = "11",
                    Phone = "982840611",
                    Email = "marceloced@gmail.com"
                }
            ];

    }
}
