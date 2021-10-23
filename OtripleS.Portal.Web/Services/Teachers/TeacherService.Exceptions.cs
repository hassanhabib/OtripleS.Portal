// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using RESTFulSense.Exceptions;

namespace OtripleS.Portal.Web.Services.Teachers
{
    public partial class TeacherService
    {
        private delegate ValueTask<IReadOnlyList<Teacher>> ReturningTeacherFunction();

        private async ValueTask<IReadOnlyList<Teacher>> TryCatch(ReturningTeacherFunction returningTeacherFunction)
        {
            try
            {
                return await returningTeacherFunction();
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

        private TeacherDependencyException CreateAndLogCriticalDependencyException(
            Exception exception)
        {
            var teacherDependencyException =
                new TeacherDependencyException(exception);

            this.loggingBroker.LogCritical(teacherDependencyException);

            return teacherDependencyException;
        }

        private TeacherDependencyException CreateAndLogDependencyException(
            Exception exception)
        {
            var teacherDependencyException =
                new TeacherDependencyException(exception);

            this.loggingBroker.LogError(teacherDependencyException);

            return teacherDependencyException;
        }

        private TeacherServiceException CreateAndLogServiceException(
            Exception exception)
        {
            var teacherServiceException =
                new TeacherServiceException(exception);

            this.loggingBroker.LogError(teacherServiceException);

            return teacherServiceException;
        }
    }
}
