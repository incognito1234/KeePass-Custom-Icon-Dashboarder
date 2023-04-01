# How to compile KeePass Custom Icon Dashboarder

1. Download and uncompress the last version of Keepass source code zip

2. Download and uncompress the KPCID source code zip or clone the repository

3. Open the solution file `KeePass\keepass.sln` in Visual Studio
   - Remove post-build command from `KeePass` and `KeePassLib` project
   - Remove signature from `KeePass` and `KeePassLib` project

4. Update the solution as follow:
   - Add the project `KPCID\CustomIconDashboarder.csproj`
   - Add the project `KPCID\LomsonLib\LomsonLib.csproj`
   - Define KeePass as starting project

5. To ensure that CustomIconDashboarder is always compile, in Visual Studio:
   - Navigate to `Debug > Options` and `Settings` or `Tools > Options`
   - Go to `Projects and Solutions > Build and Run` and make sure that the `Only build startup projects and dependencies on Run` box is left unchecked.

The solution can now be debugged by pressing `F5`