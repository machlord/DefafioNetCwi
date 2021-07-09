using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Vendedor
    {
        private int _id;
        private string _cpf;
        private string _name;
        private float _salary;

        public Vendedor()
        {
            
        }
        public Vendedor(int id, string cpf, string name, float salary)
        {
            _id = id;
            _cpf = cpf;
            _name = name;
            _salary = salary;
        }

        public int Id { get => _id; set => _id = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
        public string Name { get => _name; set => _name = value; }
        public float Salary { get => _salary; set => _salary = value; }
    }
}
