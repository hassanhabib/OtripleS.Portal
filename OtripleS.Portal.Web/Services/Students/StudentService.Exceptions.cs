// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using RESTFulSense.Exceptions;

namespace OtripleS.Portal.Web.Services.Students
{
    public partial class StudentService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();
        private delegate ValueTask<List<Student>> ReturningStudentsFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (NullStudentException nullStudentException)
            {
                throw CreateAndLogValidationException(nullStudentException);
            }
            catch (InvalidStudentException invalidStudentException)
            {
                throw CreateAndLogValidationException(invalidStudentException);
            }
            catch (HttpRequestException httpRequestException)
            {
                throw CreateAndLogCriticalDependencyException(httpRequestException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                throw CreateAndLogCriticalDependencyException(httpResponseUrlNotFoundException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                throw CreateAndLogCriticalDependencyException(httpResponseUnauthorizedException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                throw CreateAndLogDependencyValidationException(httpResponseConflictException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidStudentException = 
                    new InvalidStudentException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidStudentException);
            }
            catch (HttpResponseInternalServerErrorException httpResponseInternalServerException)
            {
                throw CreateAndLogDependencyException(httpResponseInternalServerException);
            }
            catch (HttpResponseException httpResponseException)
            {
                throw CreateAndLogDependencyException(httpResponseException);
            }
            catch (Exception serviceException)
            {
                throw CreateAndLogServiceException(serviceException);
            }
        }

        private async ValueTask<List<Student>> TryCatch(ReturningStudentsFunction returningStudentsFunction)
        {
            try
            {
                return await returningStudentsFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedStudentDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedStudentDependencyException =
                    new FailedStudentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedStudentDependencyException);
            }
            catch (Exception serviceException)
            {
                var failedStudentDependencyException =
                   new FailedStudentServiceException(serviceException);

                throw CreateAndLogServiceException(failedStudentDependencyException);
            }

        }
        private StudentValidationException CreateAndLogValidationException(Exception exception)
        {
            var studentValidationException = new StudentValidationException(exception);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }

        private StudentDependencyValidationException CreateAndLogDependencyValidationException(
            Exception exception)
        {
            var studentDependencyValidationException =
                new StudentDependencyValidationException(exception);

            this.loggingBroker.LogError(studentDependencyValidationException);

            return studentDependencyValidationException;
        }

        private StudentDependencyException CreateAndLogCriticalDependencyException(
            Exception exception)
        {
            var studentDependencyException =
                new StudentDependencyException(exception);

            this.loggingBroker.LogCritical(studentDependencyException);

            return studentDependencyException;
        }

        private StudentDependencyException CreateAndLogDependencyException(
            Exception exception)
        {
            var studentDependencyException =
                new StudentDependencyException(exception);

            this.loggingBroker.LogError(studentDependencyException);

            return studentDependencyException;
        }

        private StudentServiceException CreateAndLogServiceException(
            Exception exception)
        {
            var studentServiceException =
                new StudentServiceException(exception);

            this.loggingBroker.LogError(studentServiceException);

            return studentServiceException;
        }
    }
}
