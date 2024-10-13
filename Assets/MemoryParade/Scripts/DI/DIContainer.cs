

using System;
using System.Collections.Generic;

namespace Assets.MemoryParade.Scripts.DI
{
    /// <summary>
    /// Класс контейнеров для внедрения зависимостей с поддержкой тегов
    /// Выдает экземпляры запрашивоемого типа
    /// У дочерних контейнеров есть доступ к родительским, но не наоборот
    /// 
    /// теги. отличают экземпляры по типам 
    /// </summary>
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;

        /// <summary>
        /// список регистраций (фабрик)
        /// (string, Type) = (тег, тип)
        /// </summary>
        private readonly Dictionary<(string, Type), DIRegistration> _registration = new();
        /// <summary>
        /// для кэширования запросов на объекты, чтобы те на зацикливались 
        /// </summary>
        private readonly HashSet<(string, Type)> _resolutions = new();
        public DIContainer(DIContainer parentContainer)
        {
            _parentContainer = parentContainer;
        }

        /// <summary>
        /// Singleton указывает на то что объект 
        /// указанного типа во контейнере может быть только один. 
        /// Фабрика генерирует новый экземпляр только если он ещё не создан, 
        /// иначе возвращает существующий
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterSingleton<T>(Func<DIContainer, T> factory)
        {
            RegisterSingleton(null, factory);
        }

        public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, true);
        }

        /// <summary>
        /// Transient. Фабрика всегда будет делать новый экземпляр 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterTransient<T>(Func<DIContainer, T> factory)
        {
            RegisterTransient(null, factory);
        }

        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, false);
        }

        /// <summary>
        /// регистрирует готовый экземпляр и выдает его по запросу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// // регистрация инстанса. Снаружи создали экземпляр со своими пораметрами и запихиваем в контейнер, 
        //чтобы по запросу этого типа он выдавал данный экземпляр
        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }

        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));
            if (_registration.ContainsKey(key))
            {//если уже зарегистрировано 
                throw new Exception($"DI {key.Item1} already registration");
            }

            _registration[key] = new DIRegistration
            {
                Instance = instance,
                IsSingleton = true
            };

        }

        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool IsSingleton)
        {
            if (_registration.ContainsKey(key))
            {//если уже зарегистрировано 
                throw new Exception($"DI {key.Item1} already registration");
            }

            _registration[key] = new DIRegistration
            {
                //регистрируем выполнение фабрики 
                Factory = c => factory(c), //то же самое что и Factory = factory(c), но с апкастом T -> object
                IsSingleton = IsSingleton
            };
        }

        /// <summary>
        /// Возвращает экземпляр запрашивоемого объекта 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag"></param>
        /// <returns>Экземпляр нужного объекта типа T</returns>
        /// <exception cref="Exception"></exception>
        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutions.Contains(key))
            {
                throw new Exception($"Циклический запрос тег {tag} тип{typeof(T)}");
            }
            _resolutions.Add(key);

            try
            {
                if (_registration.ContainsKey(key))
                {
                    DIRegistration registration = _registration[key];
                    if (registration.IsSingleton)
                    {
                        if (registration.Instance == null && registration.Factory != null)
                        {
                            //то создаем Instance
                            registration.Instance = registration.Factory(this);
                        }
                        return (T)registration.Instance;
                    }
                    return (T)registration.Factory(this);
                }
                //иначе ищем в родительских контейнерах
                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            { //просто чтобы выполнилось несмотря на return 
                _registration.Remove(key);
            }

            throw new Exception($"Не найден элемент с тегом {tag}");
        }

    }
}