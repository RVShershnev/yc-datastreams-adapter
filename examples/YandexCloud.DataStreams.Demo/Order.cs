using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexCloud.DataStreams.Demo
{
    internal record Order
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
}
