using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App3
{
    class Class1
    {

        private void prueba()
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new
                {
                    ObjPersona = "algo"

                });

            }
        }

    }
}
