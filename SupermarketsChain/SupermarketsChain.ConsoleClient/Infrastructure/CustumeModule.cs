﻿namespace SupermarketsChain.ConsoleClient.Infrastructure
{
    using Ninject.Modules;

    using SupermarketsChain.Data;

    internal class CustumeModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISupermarketsChainDbContext>().To<SupermarketsChainDbContext>();
            this.Bind<ISupermarketsChainData>().To<SupermarketsChainData>();
        }
    }
}
