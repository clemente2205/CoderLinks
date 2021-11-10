# CoderLinks
Test for work
the project compilet with net core 5, use visual studio 2019 or 2022 preview, also needs nugets for entity framework .sqlserver and .sqltools
in the project is a folder called scrips, only need to execute that scrip in sql server in order to use entity framework and set datacontext with entity and that table only also in the clas dbsgdl change the line optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database="YOURDATABASE";Trusted_Connection=True;"); with your databasenames
for the end point of lifetime.
in the folder test are two screenshot of the project working

for the extra point i try to push my docker image but i have some issues with permissions, so i put docker file in the root directory with the porpouse of convert to a image
