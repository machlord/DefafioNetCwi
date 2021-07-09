using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Resultado
    {
        private int _numeroClientes;
        private int _numeroVendedores;
        private int _compraMaisCara;
        private string _piorVendedor;

        public Resultado(int numeroClientes, int numeroVendedores, int compraMaisCara, string piorVendedor)
        {
            _numeroClientes = numeroClientes;
            _numeroVendedores = numeroVendedores;
            _compraMaisCara = compraMaisCara;
            _piorVendedor = piorVendedor;
        }

        public int NumeroClientes { get => _numeroClientes; set => _numeroClientes = value; }
        public int NumeroVendedores { get => _numeroVendedores; set => _numeroVendedores = value; }
        public int CompraMaisCara { get => _compraMaisCara; set => _compraMaisCara = value; }
        public string PiorVendedor { get => _piorVendedor; set => _piorVendedor = value; }
    }
}
