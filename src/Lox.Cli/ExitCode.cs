// Copyright 1994-2023 The FreeBSD Project.
//
// Redistribution and use in source (AsciiDoc) and 'compiled' forms
// (HTML, PDF, EPUB and so forth) with or without modification, are
// permitted provided that the following conditions are met:
//
// 1. Redistributions of source code (AsciiDoc) must retain the above
//    copyright notice, this list of conditions and the following
//    disclaimer as the first lines of this file unmodified.
//
// 2. Redistributions in compiled form (Converted to PDF, EPUB and other
//    formats) must reproduce the above copyright notice, this list of
//    conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
//
// THIS DOCUMENTATION IS PROVIDED BY THE FREEBSD DOCUMENTATION PROJECT
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
// TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE FREEBSD DOCUMENTATION
// PROJECT BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY,
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS DOCUMENTATION, EVEN IF ADVISED
// OF THE POSSIBILITY OF SUCH DAMAGE.

namespace Lox
{
    /// <summary>
    /// sysexits - preferable exit codes for programs
    /// Retrieved from: https://man.freebsd.org/cgi/man.cgi?query=sysexits&apropos=0&sektion=0&manpath=FreeBSD+4.3-RELEASE&format=html
    /// </summary>
    public enum ExitCode : int
    {
        Ok = 0,
        /// <summary>
        /// The command was used incorrectly, e.g., with the
        /// wrong number of arguments, a bad flag, a bad syntax
        /// in a parameter, or whatever.
        /// </summary>
        Usage = 64,
        /// <summary>
        /// The input data was incorrect in some way. This
        /// should only be used for user's data and not system
        /// files.
        /// </summary>
        DataError = 65,
        /// <summary>
        /// An input file (not a system file) did not exist or
		/// was not readable. This could also include errors
		/// like ``No message'' to a mailer (if it cared to
		/// catch it).
        /// </summary>
        NoInput = 66,
        /// <summary>
        /// The user specified did not exist.This might be
		/// used for mail addresses or remote logins.
        /// </summary>
        NoUser = 67,
        /// The host specified did not exist. This is used in
	    /// mail addresses or network requests.
        NoHost = 68,
        /// <summary>
        /// A service is unavailable. This can occur if a sup­
        /// port program or file does not exist. This can also
        /// be used as a catchall message when something you
        /// wanted to do doesn't work, but you don't know why.
        /// </summary>
        Unavailable = 69,
        /// <summary>
        /// An internal software error has been detected. This
        /// should be limited to non-operating system related
        /// errors as possible.
        /// </summary>
        Software = 70,
        /// <summary>
        /// An operating system error has been detected.  This
        /// is intended to be used for such things as ``cannot
        /// fork'', ``cannot create pipe'', or the like.  It
        /// includes things like getuid returning a user that
        /// does not exist in the passwd file.
        /// </summary>
        OsError = 71,
        /// <summary>
        /// Some system file (e.g., /etc/passwd, /var/run/utmp,
        /// etc.) does not exist, cannot be opened, or has some
        /// sort of error (e.g., syntax error).
        /// </summary>
        OsFile = 72,
        /// <summary>
        /// A (user specified) output file cannot be created.
        /// </summary>
        CantCreate = 73,
        /// <summary>
        /// An error occurred while doing I/O on some file.
        /// </summary>
        IoError = 74,
        /// <summary>
        /// Temporary failure, indicating something that is not
        /// really an error.  In sendmail, this means that a
        /// mailer (e.g.) could not create a connection, and
        /// the request should be reattempted later.
        /// </summary>
        TempFail = 75,
        /// The remote system returned something that was ``not
        /// possible'' during a protocol exchange.
        Protocol = 76,
        /// <summary>
        /// You did not have sufficient permission to perform
        /// the operation.  This is not intended for file sys­
        /// tem problems, which should use EX_NOINPUT or
        /// EX_CANTCREAT, but rather for higher level permis­sions.
        /// </summary>
        NoPermission = 77,
        /// <summary>
        /// Something was found in an unconfigured or miscon­
        /// figured state.
        /// </summary>
        Config = 78,
    }
}
