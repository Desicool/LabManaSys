import {
  DingdingOutlined,
} from '@ant-design/icons';
import {
  Card,
  Statistic,
  Descriptions,
  Steps,
  List,
} from 'antd';
import { GridContent, PageHeaderWrapper, RouteContext } from '@ant-design/pro-layout';
import React, { Component, Fragment } from 'react';

import classNames from 'classnames';
import { connect, Dispatch } from 'umi';
import styles from './style.less';
import { IWorkFlow, IChemical } from '@/models/entity';

const { Step } = Steps;
const extra = (
  <div className={styles.moreInfo}>
    <Statistic title="状态" value="待审批" />
    <Statistic title="订单金额" value={568.08} prefix="¥" />
  </div>
);

const description = (
  <RouteContext.Consumer>
    {(value) => {
      const { isMobile } = value;
      return (
        <Descriptions className={styles.headerList} size="small" column={isMobile ? 1 : 2}>
          <Descriptions.Item label="创建人">曲丽丽</Descriptions.Item>
          <Descriptions.Item label="订购产品">XX 服务</Descriptions.Item>
          <Descriptions.Item label="创建时间">2017-07-07</Descriptions.Item>
          <Descriptions.Item label="关联单据">
            <a href="">12421</a>
          </Descriptions.Item>
          <Descriptions.Item label="生效日期">2017-07-07 ~ 2017-08-08</Descriptions.Item>
          <Descriptions.Item label="备注">请于两个工作日内确认</Descriptions.Item>
        </Descriptions>
      )
    }}
  </RouteContext.Consumer>
);

const desc1 = (
  <div className={classNames(styles.textSecondary, styles.stepDescription)}>
    <Fragment>
      曲丽丽
      <DingdingOutlined style={{ marginLeft: 8 }} />
    </Fragment>
    <div>2016-12-12 12:32</div>
  </div>
);

const desc2 = (
  <div className={styles.stepDescription}>
    <Fragment>
      周毛毛
      <DingdingOutlined style={{ color: '#00A0E9', marginLeft: 8 }} />
    </Fragment>
    <div>
      <a href="">催一下</a>
    </div>
  </div>
);
const operationTabList = [
  {
    key: 'chemicals',
    tab: '危险品列表',
  }, 
  {
    key: 'declarationForm',
    tab: '危险品申请表',
  },
  {
    key: 'financialForm',
    tab: '财务申请表',
  },
];
const ChemicalList = (loading: boolean, chemicals: IChemical[])=>(<List
  size="large"
  rowKey="id"
  loading={loading}
  dataSource={chemicals}
  renderItem={(item) => (
    <List.Item
    >
      <List.Item.Meta
        title={<span>{item.name}</span>}
        description={<div>
          <span>{(item.unitprice as number) * (item.amount as number)}</span>
          <span>{item.unitmeasurement}</span>
        </div>}
      />
    </List.Item>
  )}
/>
)
interface WorkFlowDetailProps {
  loading: boolean;
  WorkFlowDetail: IWorkFlow;
  dispatch: Dispatch
}
class WorkFlowDetail extends Component<
  WorkFlowDetailProps
  > {
  render() {
    const { WorkFlowDetail } = this.props;
    return (
      <PageHeaderWrapper
        title={'单号: ' + WorkFlowDetail.id?.toString()}
        className={styles.pageHeader}
        content={description}
        extraContent={extra}
      >
        <div className={styles.main}>
          <GridContent>
            <Card title="流程进度" style={{ marginBottom: 24 }}>
              <RouteContext.Consumer>
                {({ isMobile }) => (
                  <Steps
                    direction={isMobile ? 'vertical' : 'horizontal'}
                    current={1}
                  >
                    <Step title="创建项目" description={desc1} />
                    <Step title="部门初审" description={desc2} />
                    <Step title="财务复核" />
                    <Step title="完成" />
                  </Steps>
                )}
              </RouteContext.Consumer>
            </Card>
            <Card
              style={{ marginBottom: 24 }}
              bordered={false}
              tabList={operationTabList}
              defaultActiveTabKey='chemicals'
            >
              {ChemicalList(false, WorkFlowDetail.chemicals as IChemical[])}
            </Card>
          </GridContent>
        </div>
      </PageHeaderWrapper>
    );
  }
}
const mapStateToProps = (_ : any,ownProps: any) => {
  const {state} = ownProps.location;
  return {
    WorkFlowDetail: state.workflow,
  }
}
export default connect(
  mapStateToProps
)(WorkFlowDetail);