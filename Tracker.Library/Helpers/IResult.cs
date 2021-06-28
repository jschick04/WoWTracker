﻿using System.Collections.Generic;

namespace Tracker.Library.Helpers {

    public interface IResult {

        List<string> Messages { get; set; }

        bool Succeeded { get; set; }

    }

    public interface IResult<out T> : IResult {

        T Data { get; }

    }

}