using NerdyDuck.CodedExceptions;

[CodedException]
public class MyException : System.Exception
{
	// In addition to the standard constructors, you should define constructors that allow you to set the HResult property
	public MyException(int hresult, string message)
		: base(message)
	{
		HResult = hresult;
	}
}
