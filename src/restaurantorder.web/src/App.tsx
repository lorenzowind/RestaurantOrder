import { BrowserRouter as Router } from 'react-router-dom';

import { ConfigProvider } from 'antd';
import 'antd/dist/antd.css';

import Routes from './routes';

const App: React.FC = () => (
  <ConfigProvider>
    <Router>
      <Routes />
    </Router>
  </ConfigProvider> 
);

export default App;
