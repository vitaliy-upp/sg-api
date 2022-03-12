﻿using System.Collections.Generic;


namespace NoLimitTech.Application.ServiceInterfaces
{
    public interface IPresentationApplicationService : IApplicationService
    {
        /// <summary>
        /// Start a presentation
        /// </summary>
        /// <param name="floorsId"></param>
        void Start(IList<int> floorsId);

        /// <summary>
        /// Stop a presentation
        /// </summary>
        /// <param name="floorId"></param>
        void Stop(int floorId);
    }
}
