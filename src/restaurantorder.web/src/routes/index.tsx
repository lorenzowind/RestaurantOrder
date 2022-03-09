import { Routes as Switch, Route } from 'react-router-dom';

import Home from '../pages/Home';

const Routes: React.FC = () => (
  <Switch>
    <Route path="/" element={<Home />} />
  </Switch>
);

export default Routes;
