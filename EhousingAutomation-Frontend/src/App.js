
import { Route, Router } from 'react-router-dom';
import './App.css';

import AdminHome from './pages/AdminHome';

function App() {
  return (
     <div className="App">
      <AdminHome></AdminHome>
     </div>
    // <Router>
    //   <Route exact path="/" element={<AdminHome/>} />
    // </Router>
  );
}

export default App;
