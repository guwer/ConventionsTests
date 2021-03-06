# ConventionsTests
Constains example conventions tests to check:
* custom requiremens like using DateTime.UtcNow instead of DateTime.Now
* type dependencies between projects
* naming conventions
* assemblies references
* project structure (inheritance, namespaces etc.)
* test methods conventions

Used tools:
* Reflection
* Roslyn (parsed representation of a C# source document)
* [NetArchTest](https://github.com/BenMorris/NetArchTest)
* [Fluent Assertions](https://fluentassertions.com/)