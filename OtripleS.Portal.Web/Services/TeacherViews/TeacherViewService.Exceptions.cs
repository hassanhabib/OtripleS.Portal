// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.TeacherViews.Exceptions;

namespace OtripleS.Portal.Web.Services.TeacherViews
{
    public partial class TeacherViewService
    {
        private delegate ValueTask<List<TeacherView>> ReturningTeachersViewsFunction();

        private async ValueTask<List<TeacherView>> TryCatch(ReturningTeachersViewsFunction returningTeachersViewsFunction)
        {
            try
            {
                return await returningTeachersViewsFunction();
            }
            catch (TeacherDependencyException teacherDependencyException)
            {
                throw CreateAndLogDependencyException(teacherDependencyException);
            }
            catch (TeacherServiceException teacherServiceException)
            {
                throw CreateAndLogDependencyException(teacherServiceException);
            }
            catch (Exception serviceException)
            {
                var failedTeacherViewServiceException = 
                    new FailedTeacherViewServiceException(serviceException);

                throw CreateAndLogServiceException(failedTeacherViewServiceException);
            }
        }

        private TeacherViewDependencyException CreateAndLogDependencyException(Exception innerException)
        {
            var teacherViewDependencyException = new TeacherViewDependencyException(innerException);
            this.loggingBroker.LogError(teacherViewDependencyException);

            return teacherViewDependencyException;
        }
        private TeacherViewServiceException CreateAndLogServiceException(Exception innerException)
        {
            var teacherViewServiceException = new TeacherViewServiceException(innerException);
            this.loggingBroker.LogError(teacherViewServiceException);

            return teacherViewServiceException;
        }
    }
}
