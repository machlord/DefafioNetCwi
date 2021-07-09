using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Item
    {
        private int _id;
        private string _description;

        public Item()
        {
            
        }
        public Item(int id, string description)
        {
            _id = id;
            _description = description;
        }

        public int Id { get => _id; set => _id = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
