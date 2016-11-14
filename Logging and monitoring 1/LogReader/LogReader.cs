using System.Text;

namespace LogReader
{
	class LogReader
    {
        private string _logFileName = @"..\..\..\MvcMusicStore\logs\2016-10-31.log";

        private LogQueryClass _logQuery;
        private COMTSVInputContextClass _input;

        public LogReader()
        {
            _logQuery = new LogQueryClass();

            _input = new COMTSVInputContextClass();

            _input.headerRow = false;
            _input.iSeparator = "|";
            _input.nFields = 3;
        }

        public int GerErrorsCount()
        {
            int result = 0;
            ILogRecordset logRecordset = _logQuery.Execute($"SELECT COUNT(*) AS rowCount FROM {_logFileName} WHERE Field2 = 'ERROR'", _input);

            while (!logRecordset.atEnd())
            {
                ILogRecord record = logRecordset.getRecord();
                result = record.getValue("rowCount");

                logRecordset.moveNext();
            }

            return result;
        }

        public string GetLogMetadata()
        {
            StringBuilder result = new StringBuilder();
            ILogRecordset logRecordset = _logQuery.Execute($"SELECT Field2 as logLevel, COUNT(*) AS rowCount FROM {_logFileName} GROUP BY Field2", _input);

            while(!logRecordset.atEnd())
            {
                ILogRecord record = logRecordset.getRecord();

                if (!string.IsNullOrEmpty(record.getValue("logLevel").ToString()))
                {
                    result.Append($"Level: {record.getValue("logLevel")}; Count: {record.getValue("rowCount")}\n");
                }

                logRecordset.moveNext();
            }

            return result.ToString();
        }
    }
}
