# Anthrax

### Branch 
`release/first-prototype`. Please base all branches here.
Add a branch above branching on `release/first-prototype`

NOTE: Please name your branch
* `feature/some-feature` - for a new feature for the release
* `bugfix/some-fix` - for a bugfix for the current `release`.

Instructions:
1. Open Windows Terminal/Powershell.
2. Go to the directory

git commands:
* `git fetch` - Update the meta data about the remote git repository(see if you're behind on commits or if there's a new branch. Use `git fetch` to update your local git repo when you create your own branch.
* `git checkout <branch_name>` - To switch branches
* `git pull origin <branch_name>` - To update your local git repository. Use this when you create a new branch
* `git push origin <branch_name>` - To push your commit to the remote git repository.

Then, create a pull request to pull your branch to the `release` branch. I will merge when it's safe. 

### Why create a branch for every feature?
The purpose of this is since we're working separately, if we both work on the `release` branch at the same time, or if we have our "own" branches but we will only commit when it's
finished, there will be merge conflicts after. Therefore, only one person should work on one file to avoid merge conflicts. If you see something wrong with my code, tell me instead
of fixing the bug as that will result to merge conflicts which are a huge pain in the ass.
