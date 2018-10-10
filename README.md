
## Prerequesite

This solution will need a (localdb)\MSSQLLocalDB 
for creating a "TUI.Ado.ENtity.Source.TuiContext" database for the solution, 
and a "TuiTest" database for unit tests 
and a "TuiTest2" for the integration tests

The test are made with NUNIT, so be sure to have required plugins 
for executing them from visual studio (for my case, I use axoCover which works like a charm)

After to checkout the solution, I highly recommend to BUILD the solution once in order to
make visual studio download the required nugets for running the solution. If visual studio cannot download nugets
(which is not supposed to happen). You can checkout the package of TUI.project from this url:
https://github.com/ITbob/TUI.packages
then put the package folder at the root of TUI.Project

## Context

TUI.Project is composed of three folders:
Business: contains extension methods which handle gaz calculations, utc calculations etc...
DAL: contains a repository project and a project which implements the repository project with entity framework
Model: obviously contains all the model (city, airport, flight etc...)

Note for UTC calculation: the project is currently using google api for making the calculation. 
However, google api recently often rejects my requests by sending back a "OVER_QUERY_LIMIT".
I highly recommend to use a google api key.

## How to run the project
just execute the solution from visual studio and run your favorite browser.

## Running the project
if you wish to change data, you need to create a user or use the existing account (name: bob password:bob)



