// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using RESTFulSense.Exceptions;

namespace OtripleS.Portal.Web.Services.StudentViews
{
    public partial class StudentViewService
    {
        private delegate ValueTask<StudentView> ReturningStudentViewFunction();
        private delegate void ReturningNothingFunction();
        private delegate ValueTask<List<StudentView>> ReturningStudentsViewFunction();

        private async ValueTask<StudentView> TryCatch(ReturningStudentViewFunction returningStudentViewFunction)
        {
            try
            {
                return await returningStudentViewFunction();
            }
            catch (NullStudentViewException nullStudentViewException)
            {
                throw CreateAndLogValidationException(nullStudentViewException);
            }
            catch (InvalidStudentViewException invalidStudentViewException)
            {
                throw CreateAndLogValidationException(invalidStudentViewException);
            }
            catch (StudentValidationException studentValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentValidationException);
            }
            catch (StudentDependencyValidationException studentDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentDependencyValidationException);
            }
            catch (StudentDependencyException studentDependencyException)
            {
                throw CreateAndLogDependencyException(studentDependencyException);
            }
            catch (StudentServiceException studentServiceException)
            {
                throw CreateAndLogDependencyException(studentServiceException);
            }
            catch (Exception serviceException)
            {
                throw CreateAndLogServiceException(serviceException);
            }
        }

        private void TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                returningNothingFunction();
            }
            catch (InvalidStudentViewException invalidStudentViewException)
            {
                throw CreateAndLogValidationException(invalidStudentViewException);
            }
            catch (Exception serviceException)
            {
                throw CreateAndLogServiceException(serviceException);
            }
        }

        private async ValueTask<List<StudentView>> TryCatch(ReturningStudentsViewFunction returningStudentsViewFunction)
        {
            try
            {
                return await returningStudentsViewFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var studentDependencyException =
                    new StudentDependencyException(httpRequestException);

                throw CreateAndLogDependencyException(studentDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var studentDependencyException =
                    new StudentDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogDependencyException(studentDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var studentServiceException =
                    new StudentDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogDependencyException(studentServiceException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var studentServiceException =
                    new StudentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(studentServiceException);
            }
            catch (Exception serviceException)
            {
                var studentServiceException =
                    new StudentServiceException(serviceException);

                throw CreateAndLogServiceException(studentServiceException);
            }
        }

        private StudentViewValidationException CreateAndLogValidationException(Exception exception)
        {
            var studentViewValidationException = new StudentViewValidationException(exception);
            this.loggingBroker.LogError(studentViewValidationException);

            return studentViewValidationException;
        }

        private StudentViewDependencyValidationException CreateAndLogDependencyValidationException(Exception exception)
        {
            var studentViewDependencyValidationException = new StudentViewDependencyValidationException(exception);
            this.loggingBroker.LogError(studentViewDependencyValidationException);

            return studentViewDependencyValidationException;
        }

        private StudentViewDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var studentViewDependencyException = new StudentViewDependencyException(exception);
            this.loggingBroker.LogError(studentViewDependencyException);

            return studentViewDependencyException;
        }

        private StudentViewServiceException CreateAndLogServiceException(Exception exception)
        {
            var studentViewServiceException = new StudentViewServiceException(exception);
            this.loggingBroker.LogError(studentViewServiceException);

            return studentViewServiceException;
        }
    }
}
