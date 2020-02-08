using System;
using System.Threading.Tasks;

namespace Kinopub.Api.Tools
{
    /// <summary>
    /// Методы расширения для типов
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Попытаться асинхронно выполнить метод.
        /// </summary>
        /// <param name="func"> Синхронный метод. </param>
        /// <typeparam name="T"> Тип ответа </typeparam>
        /// <returns> Результат выполнения функции. </returns>
        public static Task<T> TryInvokeMethodAsync<T>(Func<T> func)
        {
        #if NET40
			return TaskEx.Run(func);
        #else
            return Task.Run(func);
        #endif
        }

        /// <summary>
        /// Попытаться асинхронно выполнить метод.
        /// </summary>
        /// <param name="func"> Синхронный метод. </param>
        /// <returns> Результат выполнения функции. </returns>
        public static Task TryInvokeMethodAsync(Action func)
        {
        #if NET40
			return TaskEx.Run(func);
        #else
            return Task.Run(func);
        #endif
        }
	}
}