Orc.CsvHelper
=================

Small library of extensions and helper methods for the [CsvHelper](http://joshclose.github.io/CsvHelper) library.

- Static methods are provided to read or write to a csv file with one line of code.
- Sensible configuration options are provided out of the box (but can be overwitten if needed).
- Reading from a csv file will not lock it.
- Capture a meaningful error message when an exception is thrown while reading a csv file. 

Features
----------

- **code generaion** => Use static class **CodeGeneration** for generating C# POCO classes and their associated maps.
- **CsvReader helper** => Use static class **CsvReaderHelper** for reading csv files with a single line of code.
- **CsvWriter helper** => Use static class **CsvWriterHelper** for writing records into a csv file.
- **CsvExtensions** => for writing object collections into a csv file using the **ToCsv()** method.
