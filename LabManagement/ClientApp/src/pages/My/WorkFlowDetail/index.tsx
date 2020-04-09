import {
  DingdingOutlined,
} from '@ant-design/icons';
import {
  Card,
  Statistic,
  Descriptions,
  Steps,
  List,
  Tabs,
  Collapse,
} from 'antd';
import 'antd/dist/antd.css';
import { GridContent, PageHeaderWrapper, RouteContext } from '@ant-design/pro-layout';
import React, { Fragment, FC, useEffect } from 'react';

import classNames from 'classnames';
import { connect, Dispatch, WorkFlowDetailStateType } from 'umi';
import styles from './style.less';
import { IChemical, IFinancialForm, IDeclarationForm } from '@/models/entity';

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
const ChemicalList = (param: {
  loading: boolean;
  chemicals: IChemical[];
}) => (<List
  size="large"
  rowKey="id"
  loading={param.loading}
  dataSource={param.chemicals}
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
const FinancialFormCollapse = (param: {
  loading: boolean;
  financialForms: IFinancialForm[];
}) => (
    <Collapse>
      {param.financialForms.map(u => (
        <Collapse.Panel header={'收款方：' + u.receiver} key={u.id as number}>
          <Descriptions bordered>
            <Descriptions.Item label='申请人'>{u.uname}</Descriptions.Item>
            <Descriptions.Item label='实验室编号'>{u.lid}</Descriptions.Item>
            <Descriptions.Item label='当前状态'>{u.state}</Descriptions.Item>
            <Descriptions.Item label='金额'>{u.price?.toString() + '元'}</Descriptions.Item>
            <Descriptions.Item label='提交时间'>{u.stime}</Descriptions.Item>
            <Descriptions.Item label='收款方'>{u.receiver}</Descriptions.Item>
          </Descriptions>
        </Collapse.Panel>
      ))}
    </Collapse>
  )
const DeclarationFormDescription = (param: {
  declarationForm: IDeclarationForm;
}) => (
    <Descriptions bordered>
      <Descriptions.Item label='申请人'>{param.declarationForm.uname}</Descriptions.Item>
      <Descriptions.Item label='实验室编号'>{param.declarationForm.lid}</Descriptions.Item>
      <Descriptions.Item label='当前状态'>{param.declarationForm.state}</Descriptions.Item>
      <Descriptions.Item label='理由' span={2}>{param.declarationForm.reason}</Descriptions.Item>
      <Descriptions.Item label='处理人'>{param.declarationForm.hid}</Descriptions.Item>
      <Descriptions.Item label='提交时间'>{param.declarationForm.stime}</Descriptions.Item>
    </Descriptions>
  )
interface WorkFlowDetailProps {
  loading: boolean;
  workFlowDetailState: WorkFlowDetailStateType;
  dispatch: Dispatch;
  id: number;
}
const WorkFlowDetail: FC<
  WorkFlowDetailProps
> = (props) => {
  const { dispatch, loading } = props;
  useEffect(() => {
    if (props.workFlowDetailState.currentWorkFlow === undefined) {
      dispatch({
        type: 'workFlowDetail/fetchWorkFlow',
        payload: props.id
      });
    }
  }, []);
  const { currentWorkFlow, declarationForm, financialForms } = props.workFlowDetailState || {};
  console.log(currentWorkFlow);
  console.log(props.workFlowDetailState);
  return (
    <PageHeaderWrapper
      title={'单号: ' + props.id.toString()}
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
            bordered={false}
          >
            <Tabs>
              <Tabs.TabPane tab='危险品列表' key='chemicals'>
                <ChemicalList
                  loading={loading}
                  chemicals={currentWorkFlow?.chemicals || []}
                />
              </Tabs.TabPane>
              <Tabs.TabPane tab='财务表单' key='financials'>
                <FinancialFormCollapse loading={loading} financialForms={financialForms} />
              </Tabs.TabPane>
              <Tabs.TabPane tab='申请表' key='declaration'>
                <DeclarationFormDescription declarationForm={declarationForm || {}}/>
              </Tabs.TabPane>
            </Tabs>
          </Card>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
}
const mapStateToProps = ({
  loading,
  workFlowDetail
}: {
  workFlowDetail: WorkFlowDetailStateType;
  loading: {
    models: { [key: string]: boolean };
  };
}, ownProps: any) => {
  const id = Number.parseInt(ownProps.match.params.workflowid);
  return {
    loading: loading.models.currentWorkFlow,
    workFlowDetailState: workFlowDetail,
    id
  }
}
export default connect(
  mapStateToProps
)(WorkFlowDetail);