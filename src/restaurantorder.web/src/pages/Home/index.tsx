import { useCallback, useState } from 'react';
import { 
  Layout, 
  Breadcrumb, 
  Row, 
  Col, 
  Button,
  List,
  notification,
  Spin,
  Input,
  Typography 
} from 'antd';
import { 
  LoadingOutlined
} from '@ant-design/icons';


import api from '../../services/api';

const { Title } = Typography;
const { Content } = Layout;

const Movies: React.FC = () => {
  const [order, setOrder] = useState('');
  const [outputOrder, setOutputOrder] = useState('');

  const [orders, setOrders] = useState<string[]>([]);
  
  const [loading, setLoading] = useState(false);

  const handleOrderValidation = useCallback(async () => {
    try {
      setLoading(true);

      const response = await api.post(`validate-order?order=${order}`);

      if (response) {
        setOutputOrder(response.data);
        setOrders([...orders, response.data]);

        notification.success({
          message: 'Restaurant order validated successfully',
          placement: 'bottomRight',
        });

        setLoading(false);
      }
    } catch (e) {
      notification.error({
        message: 'An error occurred!',
        description: 'Check if the order is well-formed'
      });
      
      setOutputOrder("");
    } finally {
      setLoading(false);
    }
  }, [order]);

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Layout className="site-layout">
        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb style={{ margin: '16px 0' }}>
            <Breadcrumb.Item>
              Restaurant Orders
            </Breadcrumb.Item>
          </Breadcrumb>

          <div 
            className="site-layout-background" 
            style={{ padding: 24, minHeight: 360, background: "#fff" }}
          >
            <Row gutter={[8, 24]}>
              <Col span={10}>
              <Input.Group compact>
                <Input 
                  style={{ width: 'calc(100% - 200px)' }} 
                  placeholder={"Type the restaurant order here..."} 
                  value={order}
                  onChange={e => setOrder(e.target.value)}
                />

                <Button 
                  onClick={handleOrderValidation} 
                  type="primary">
                    Validate
                </Button>
              </Input.Group>
              
              </Col>
              <Col span={13}/>
              <Col span={1}>
                <Spin 
                  spinning={loading} 
                  indicator={<LoadingOutlined style={{ fontSize: 24 }} spin />} 
                />
              </Col>
            </Row>

            <Title level={4} style={{ padding: 24 }}>
              Output: {outputOrder}
            </Title>
            
            <List
              itemLayout="horizontal"
              dataSource={orders}
              renderItem={(order, index) => (
                <List.Item>
                  <List.Item.Meta
                    title={`Order ${index}`}
                    description={order}
                  />
                </List.Item>
              )}
            />
          </div>
        </Content>
      </Layout>
    </Layout>
  );

}

export default Movies;
