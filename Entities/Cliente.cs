using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Cliente
    {
        private int _id;
        private string _cnpj;
        private string _name;
        private string _businessArea;

        public Cliente()
        {
            
        }

        public Cliente(int id, string cnpj, string name, string businessArea)
        {
            _id = id;
            _cnpj = cnpj;
            _name = name;
            _businessArea = businessArea;
        }

        public int Id { get => _id; set => _id = value; }
        public string Cnpj { get => _cnpj; set => _cnpj = value; }
        public string Name { get => _name; set => _name = value; }
        public string BusinessArea { get => _businessArea; set => _businessArea = value; }
    }
}
