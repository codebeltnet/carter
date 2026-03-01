$version = minver -i -t v -v w
docker tag carter-docfx:$version jcr.codebelt.net/geekle/carter-docfx:$version
docker push jcr.codebelt.net/geekle/carter-docfx:$version
