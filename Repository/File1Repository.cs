using convertTmplate.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace convertTmplate.Repository
{
    public class File1Repository
    {
        ConvertEntity Context = new ConvertEntity();
        public void Add(File1 model)
        {
            Context.File1.Add(model);
            Context.SaveChanges();
        }
    }
    public class File2Repository
    {
        ConvertEntity Context = new ConvertEntity();
        public void Add(File2 model)
        {
            Context.File2.Add(model);
            Context.SaveChanges();
        }
    }
    public class File3Repository
    {
        ConvertEntity Context = new ConvertEntity();
        public void Add(File3 model)
        {
            Context.File3.Add(model);
            Context.SaveChanges();
        }
    }
}