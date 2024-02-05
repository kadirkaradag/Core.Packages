using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler  //implementasyon bunu implemente edecek yerde olacak o yüzden abstract class olacak
{
    public Task HandleExceptionAsync(Exception exception) =>
        exception switch
        {
            BusinessException businessException => HandleException(businessException), // gelen exception BusinessException businessException türündeyse HandleException(businessException), diğer caselerde _ kısmı calısacak.
            ValidationException validationException => HandleException(validationException),
            _ => HandleException(exception)
        };

    protected abstract Task HandleException(BusinessException businessException); // abstract olmasının sebebi inherit eden sınıf doldurmak zorunda olsun
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(Exception exception); // diğer durumlarda bu calısacak
}
