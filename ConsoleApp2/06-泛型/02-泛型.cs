namespace ConsoleApp2._06_泛型
{
	// 必须要有无参构造参数
	class Factory<T> where T : new()
	{
		public static T Create()
		{
			return new T();
		}
	}

	// 定义一个接口
	interface ILogger
	{
		void Log();
	}

	// 必须要实现 ILogger 接口
	class Service<T> where T : ILogger
	{
		public static void Run(T logger)
		{
			logger.Log();
		}
	}

	// 定义一个泛型接口
	interface IRepository<T>
	{
		// 允许返回的结果为null
		T? GetById(int id);
		void Add(T entity);
	}

	class UserRepository : IRepository<User>
	{
		public User GetById(int id) 
		{
			return new User();
		}

		public void Add(User entity) { }
	}
}
