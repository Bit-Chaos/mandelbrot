# use the dotnet 5.0 preview sdk
FROM mcr.microsoft.com/dotnet/core/sdk:5.0

# Avoid warnings by switching to noninteractive
ENV DEBIAN_FRONTEND=noninteractive
# This Dockerfile adds a non-root 'vscode' user with sudo access. However, for Linux,
# this user's GID/UID must match your local user UID/GID to avoid permission issues
# with bind mounts. Update USER_UID / USER_GID if yours is not 1000. See
# https://aka.ms/vscode-remote/containers/non-root-user for details.
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

## # Configure apt and install packages
## # make several small steps to improve layer caching...
RUN apt-get update
RUN apt-get -y install --no-install-recommends apt-utils dialog 2>&1
    #
    # Verify git, process tools, lsb-release (common in install instructions for CLIs) is installed
RUN apt-get -y install git iproute2 procps lsb-release 2>&1
    # verify the C++ components as well being installed
RUN apt-get -y install cmake make gcc gdb
RUN apt-get -y install python3-pip
RUN pip3 install conan
    #
    # Install the tools we need for our KNOR development
RUN apt-get -y install vim-nox tmux nmap 2>&1 

RUN useradd -ms /bin/bash ${USERNAME}
RUN mkdir -p /opt/kratos/Logs \
    && mkdir -p /opt/kratos/Bin \
    && chgrp -R vscode /opt/kratos \
    && chmod -R g+wr /opt/kratos \
    && mkdir -p /home/vscode/.nuget/NuGet \
    && chown -R vscode:vscode /home/vscode/.nuget \
    && mkdir -p /home/vscode/.conan \
    && chown -R vscode:vscode /home/vscode/.conan
USER ${USERNAME}
WORKDIR /home/${USERNAME}

# Switch back to dialog for any ad-hoc use of apt-get
ENV DEBIAN_FRONTEND=

# create the environment variables
# set the opt-out variable for the .NET SDK
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
