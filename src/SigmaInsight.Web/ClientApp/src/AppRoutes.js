import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import OpenAiNoRefinement from "./components/OpenAiNoRefinement";
import OpenAiPromptEngineering from "./components/OpenAiPromptEngineering";
import OpenAiFineTuned from "./components/OpenAiFineTuned";
import OpenAiPage from "./components/OpenAiPage";
import Controllers from "./components/OpenAiControllers";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/no-refinement',
    element: <OpenAiPage controller={Controllers.noRefinement} />
  },
  {
    path: '/prompt-engineering',
    element: <OpenAiPage controller={Controllers.promptEngineering} />
  },
  {
    path: '/fine-tuned',
    element: <OpenAiPage controller={Controllers.fineTuned} />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
