# Power diary technical test

## Introduction

The repository contains the implementation for the technical test for software engineer position at Power Diary.

## Assumptions

Based on the description of the problem, the following assumptions were made:
- When the user selects to see the events by minute it's treated on the same way as the rest of granularity options.
This means that if two events happen in the same minute they are displayed as
```
        George enters the room
18:15   Bob leaves
        John high-fives Alice
```
It would be easy to change if we would like to display the events in a different way e.g. have the time before each event.

## Technical choices

The application is implemented using latest .NET, React.js and Next.js. The backend is implemented using C# and the frontend is implemented using TypeScript, React and Next.js.

For persisting the data SQLite, which is an in-memory relational database is used. Database migrations have been
added and seed data is added to the database the first time we start the application.

Because some advanced grouping is needed SQLite can't handle it so actually we fetch all the data from the
database and then we group it in memory. This is not viable for a production application but for the purpose of
this test it's good enough.

## Further improvements

Pagination of the data has not been taken into account as part of the test. In a real world application we should
add support pagination and most likely some filtering when fetching the data otherwise we will end up fetching
way too much data that it would be hard for the user to navigate around.

Building and executing the application in release mode, ready for production has not been implemented.

## Executing locally

To be able to execute the application and the tests locally you need to have install dotnet 8 and node js, preferably the latest version.

As part of the repo a well known solution file exists that can be used to open the application in Visual Studio or Rider. From there it should be relatively easy to start the application or execute the tests.

In case you want to proceed with just command line and you don't want to use an IDE, you can use the following commands.

After cloning the repository, you should execute the following commands begin the application:

From the root of the repository:
```bash
dotnet restore
```
to restore the dotnet dependencies.

Then, to start the application:
```bash
dotnet run --project ./PowerDiary/PowerDiary.csproj
```

By default this starts `http` profile.

Once this succeeds then you can access the swagger UI of the API at

```
http://localhost:5268/swagger/index.html
```

From here you can actually test the application using Swagger UI.

## UI access

A basic UI is also available as part of the test. The following should be done despite the usage of IDE or not.

To be able to access it you need to navigate to the `PowerDiary/client-app` folder and execute the following commands:

```bash
npm install
```

and then
```bash
npm run dev
```

This starts Next.js development server and you can access the UI at the root of the application.

```
http://localhost:5268/
```

There a basic UI where you can select the granularity of the events and see the events history.

An invalid granularity exists just to demonstrate how we handle errors.

## Tests execution

As part of the application a few unit tests exist. To be able to execute them you need to navigate to the `PowerDiary.Tests` folder and execute the following command:

```bash
dotnet test
```

After executing this command you should see the results of the tests.
