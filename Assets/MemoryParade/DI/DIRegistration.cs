using System;
using System.Collections.Generic;

namespace DI{
    class DIRegistration{
        /// <summary>
        /// делигат 
        /// фабрика создает пустые конструкторы, если внутрь не добавить нужные параметры
        /// </summary>
        public Func<DIContainer, object> Factory{ get; set; }
        public bool IsSingleton{ get; set; }
        
        /// <summary>
        /// опциональная ссылка
        /// </summary>
        public object Instance{ get; set; }
    }
}


