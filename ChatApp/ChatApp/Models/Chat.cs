﻿using System;
using System.Collections.Generic;

namespace ChatApp.Models
{
    
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> users { get; set; }
        public ChatType Type { get; set; }
    }

   
    
}