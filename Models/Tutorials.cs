using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace ExampleApi.Models
{
    public class Tutorials
    {
        public long Id {get;set;}
        public String Title { get; set; }
        public String Description {get;set;}
        public bool Published {get;set;}


        public void Add (Tutorials tutorials)
        {

        }

    }
}