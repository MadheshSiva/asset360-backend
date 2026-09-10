import { Container, getContainer } from "@cloudflare/containers";

export class AssetContainer extends Container {
  defaultPort = 5296; // ApiGateway's existing port, unchanged
  sleepAfter = "30m"; // longer idle window = fewer Mongo data resets (disk is wiped on sleep)
}

export default {
  async fetch(request, env) {
    const container = getContainer(env.ASSET_CONTAINER);
    return container.fetch(request);
  },
};
