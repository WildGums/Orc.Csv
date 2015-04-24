Orc.CsvHelper
=================

[![Join the chat at https://gitter.im/WildGums/Orc.CsvHelper](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/WildGums/Orc.CsvHelper?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Small library of extensions and helper methods for the [CsvHelper](http://joshclose.github.io/CsvHelper) library.

- Static methods are provided to read or write to a csv file with one line of code.
- Sensible configuration options are provided out of the box (but can be overwritten if needed).
- Reading from a csv file will not lock it.
- Capture a meaningful error message when an exception is thrown while reading a csv file. 

Features
----------

- **code generaion** => Use static class **CodeGeneration** for generating C# POCO classes and their associated maps.
- **CsvReader helper** => Use static class **CsvReaderHelper** for reading csv files with a single line of code.
- **CsvWriter helper** => Use static class **CsvWriterHelper** for writing records into a csv file.
- **CsvExtensions** => for writing object collections into a csv file using the **ToCsv()** method.
