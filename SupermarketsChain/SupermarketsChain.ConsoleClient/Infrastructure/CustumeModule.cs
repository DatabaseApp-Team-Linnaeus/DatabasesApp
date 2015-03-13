using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketsChain.ConsoleClient.Infrastructure
{
    using Ninject.Modules;

    class CustumeModule : INinjectModule
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void OnLoad(Ninject.IKernel kernel)
        {
            throw new NotImplementedException();
        }

        public void OnUnload(Ninject.IKernel kernel)
        {
            throw new NotImplementedException();
        }

        public void OnVerifyRequiredModules()
        {
            throw new NotImplementedException();
        }

        public Ninject.IKernel Kernel
        {
            get { throw new NotImplementedException(); }
        }
    }
}
