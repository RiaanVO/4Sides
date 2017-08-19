# SECTOR STRIKE

## Branch Structure
We'll utilise multiple Git branches for the development of this project and each branch will have a different purpose. Our branching structure is based on [Git Flow](http://nvie.com/posts/a-successful-git-branching-model/). I recommend skimming the Git Flow article if you're not familiar with the workflow.

The `master` branch represents the most recent 'release' of our game. Obviously, our game won't be publically available for download, but each commit in the `master` branch represents a build of the game for a particular playtest.

The `develop` branch is the integration branch for `master`. When we want to add new functionality to the game, we create a feature branch off of `develop`. When this feature is complete, we merge our feature branch into `develop`. By ensuring everyone's developing on their own branch, we reduce the likelihood of stepping on eachother's toes.

Once we're ready for a playtest, we create a branch off of `develop` for that particular release (prototype, alpha, beta, final). In the release branch, we fix any immediate issues, attach a new version number and then merge it into `master`. We also merge it into `develop` to ensure any fixes made in the release branch are reflected in the most up-to-date version of the game (`develop` branch).