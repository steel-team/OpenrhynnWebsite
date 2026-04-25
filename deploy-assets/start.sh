#!/bin/bash
export NODE_ENV=production
# listen address, highly recommended to change it to localhost and hide nuxt server behind nginx proxy for example
export HOST=127.0.0.1
# listen port
export PORT=8088
# run in cluster mode
export NITRO_PRESET=node_cluster
# url on which frontend available, used for SEO, meta tags etc.
export NUXT_PUBLIC_SITE_URL=https://openrhynn.net
export NUXT_PUBLIC_I18N_BASE_URL=https://openrhynn.net

#/home/$USER/.bun/bin/bun run ./server/index.mjs
# deno task --config .output/deno.json start
#/home/$USER/.deno/bin/deno task --config ./deno.json start

export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"
nvm use 25
node ./server/index.mjs
