# miniBackend
  mini-program backend,  webapi (.net core) version, also support deploy to aws elastic beanstalk.

## for aws deployment
### docker build, given your image name: yufelix/boapp
  docker build -t yufelix/boapp .

### docker run, run image with name: yufelix/boapp
  docker run -it --rm -p 80:80 yufelix/boapp .

### docker push, given your repository name in docker.io: yufelix/boapp
  docker push yufelix/boapp

## other commands

### check all containers in your vm
  docker container ls 

### kill and remove container by container id
  docker container kill 16e33b3aa05c
  docker container rm 16e33b3aa05c

### remove all stopped containers
  docker container prune

### check all images in your vm
  docker images
  docker image rm 9b4414a0851

### remove all unused containers
  docker image prune
