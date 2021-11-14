using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Courses;
using OtripleS.Portal.Web.Models.Courses.Exceptions;
using RESTFulSense.Exceptions;

namespace OtripleS.Portal.Web.Services.Courses
{
    public partial class CourseService
    {
        private delegate ValueTask<List<Course>> ReturningCoursesFunction();

        private async ValueTask<List<Course>> TryCatch(ReturningCoursesFunction returningCoursesFunction)
        {
            try
            {
                return await returningCoursesFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCourseDependencyException =
                    new FailedCourseDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCourseDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCourseDependencyException =
                    new FailedCourseDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCourseDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCourseDependencyException =
                    new FailedCourseDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCourseDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedCourseDependencyException =
                    new FailedCourseDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedCourseDependencyException);
            }
        }

        private CourseDependencyException CreateAndLogCriticalDependencyException(
            Exception exception)
        {
            var courseDependencyException =
                new CourseDependencyException(exception);

            this.loggingBroker.LogCritical(courseDependencyException);

            return courseDependencyException;
        }

        private CourseDependencyException CreateAndLogDependencyException(
            Exception exception)
        {
            var courseDependencyException =
                new CourseDependencyException(exception);

            this.loggingBroker.LogError(courseDependencyException);

            return courseDependencyException;
        }

    }
}
