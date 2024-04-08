import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import OpenAiNoRefinement from "./components/OpenAiNoRefinement";
import OpenAiPromptEngineering from "./components/OpenAiPromptEngineering";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/no-refinement',
    element: <OpenAiNoRefinement />
  },
  {
    path: '/prompt-engineering',
    element: <OpenAiPromptEngineering />
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
