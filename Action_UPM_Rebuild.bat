git branch -D upm
git push origin -d upm
git subtree split --prefix=Unity/Assets/FlowEditor --branch upm
git push origin upm --tags