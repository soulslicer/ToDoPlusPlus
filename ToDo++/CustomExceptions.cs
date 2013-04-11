//@raaj A0081202Y
using System;

namespace ToDo
{
    public class TextSizeOutOfRangeException : Exception { public TextSizeOutOfRangeException(string message) : base(message) { } }
    public class RepeatCommandException : Exception { public RepeatCommandException(string message) : base(message) { } }
    public class NothingSelectedException : Exception { public NothingSelectedException(string message) : base(message) { } }
    public class InvalidDateTimeException : Exception { public InvalidDateTimeException(string message) : base(message) { } }
    public class InvalidDeleteFlexiException : Exception { public InvalidDeleteFlexiException(string message) : base(message) { } }
    public class InvalidTimeRangeException : Exception { public InvalidTimeRangeException(string message) : base(message) { } }
    public class MultipleCommandsException : Exception { public MultipleCommandsException() : base() { } }
    public class TaskFileCorruptedException : Exception { public TaskFileCorruptedException() : base() { } }
    public class TaskHasNoDayException : Exception { public TaskHasNoDayException() : base() { } }
}
