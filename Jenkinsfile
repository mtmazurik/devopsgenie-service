pipeline {
  environment {
    registry = "mtmazurik/devopsgenie-service"
    registryCredential = 'DockerHub'
  }
  agent any
  stages {
    stage('Pull DevopsGenie Service source code') {
      steps {
        git 'https://github.com/mtmazurik/devopsgenie-service.git'
      }
    }
    stage('Building Docker image') {
      steps{
        script {
          dockerImage = docker.build registry + ":latest"
        }
      }
    }
    stage('Push Docker Image to DockerHub') {
      steps{
        script {
          docker.withRegistry( '', registryCredential) {
            dockerImage.push()
          }
        }
      }
    }
  }
}
