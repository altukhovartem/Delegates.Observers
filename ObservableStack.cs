using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
	public class StackOperationsLogger
	{
		private readonly Observer observer = new Observer();
		
		public void SubscribeOn<T>(ObservableStack<T> stack)
		{
			stack.Add(observer);
		}

		public string GetLog()
		{
			return observer.Log.ToString();
		}
	}

	//public interface IObserver
	//{
	//	void HandleEvent(object eventData);
	//}

	public class Observer
	{
		public StringBuilder Log = new StringBuilder();

		public void HandleEvent(object eventData)
		{
			Log.Append(eventData);
		}
	}

	//public interface IObservable
	//{
	//	void Add(IObserver observer);
	//	void Remove(IObserver observer);
	//	//void Notify(object eventData);

	//	event MyDelegate NotifyEvent;
	//}

	public delegate void MyDelegate(object obj);

	public class ObservableStack<T>
	{
		List<Observer> observers = new List<Observer>();
		public event MyDelegate NotifyEvent;

		public void Add(Observer observer)
		{
			NotifyEvent += observer.HandleEvent;
		}

		//public void Notify(object eventData)
		//{
		//	foreach (var observer in observers)
		//		observer.HandleEvent(eventData);
		//}

		public void Remove(Observer observer)
		{
			NotifyEvent -= observer.HandleEvent;
		}

		List<T> data = new List<T>();

		public void Push(T obj)
		{
			data.Add(obj);
			NotifyEvent(new StackEventData<T> { IsPushed = true, Value = obj });
		}

		public T Pop()
		{
		 	if (data.Count == 0)
		 		throw new InvalidOperationException();
		 	var result = data[data.Count - 1];
		 	NotifyEvent(new StackEventData<T> { IsPushed = false, Value = result });
		 	return result;
			
		}
	}
}
