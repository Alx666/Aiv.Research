# Aiv.Research

#### Git Noob Spellbook

![penta.jpg](/penta.jpg)

Some useful commands for git noobs.
Git experts, please stay away for your own safety.

* Checking current branch status
```
git status
```

* Commit history
```
/* Basic version */
git log

/* Nose-bleeding sexy version */
git log --graph --abbrev-commit --decorate
```


* Get last update version of the current branch

```
git pull
```

* Add files

```
/* Add a single file */
git add path/to/file.ext	

/* Add all files */
git add .
```

* Delete files and track its deletion

```
/* Delete a single file and remove it from repository */
git rm -f path/to/file.ext

/* Delete local modified version of all files */
git rm -rf --cached . 
```

* Bruthal reset / changes genocide*
```
/* This will completely bomb any local change and reset everything to HEAD revision */
git reset --hard HEAD
```

** Available options
```
-f		// forced
-r		// recursively (for deleting all content in a folder)
```

* Commit changes (locally)

```
git commit -m "Your comment to this commit"
```

* Updating remote version with your changes

```
git push
```
