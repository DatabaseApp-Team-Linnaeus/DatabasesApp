namespace SupermarketsChain.ConsoleClient.Infrastructure
{
    using System;

    using Ninject;

    public static class ObjectFactory
    {
        private static readonly IKernel MKernel = new StandardKernel(new CustumeModule());

        /// <summary>
        /// Static accessor to the Ninject kernel
        /// </summary>
        public static IKernel Kernel
        {
            get { return MKernel; }
        }

        /// <summary>
        /// Gets an object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to get</typeparam>
        /// <returns>The object if successful, throws an exception if failed</returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Gets an object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="type">The type of the object to get</param>
        /// <returns>The object if successful, throws an exception if failed</returns>
        public static T Get<T>(Type type)
        {
            return (T)Kernel.Get(type);
        }

        /// <summary>
        /// Gets a named object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to get</typeparam>
        /// <param name="name">The name of the object</param>
        /// <returns>The object if successful, throws an exception if failed</returns>
        public static T Get<T>(string name)
        {
            return Kernel.Get<T>(name);
        }

        /// <summary>
        /// Trys to get an object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to get</typeparam>
        /// <returns>The object if successful, null if failed</returns>
        public static T TryGet<T>()
        {
            return Kernel.TryGet<T>();
        }

        /// <summary>
        /// Tries to get an object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <param name="type">The type of the object to get</param>
        /// <returns>The object if successful, throws an exception if failed</returns>
        public static T TryGet<T>(Type type)
        {
            return (T)Kernel.TryGet(type);
        }

        /// <summary>
        /// Tryies to get an object using Ninject
        /// </summary>
        /// <typeparam name="T">The type of object to get</typeparam>
        /// <param name="name">The name of the object</param>
        /// <returns>The object if successful, null if failed</returns>
        public static T TryGet<T>(string name)
        {
            return Kernel.TryGet<T>(name);
        }
    }
}
