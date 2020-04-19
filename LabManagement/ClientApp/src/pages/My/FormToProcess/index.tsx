import {
  Card,
  Statistic,
  Descriptions,
  Divider,
  Tooltip,
  Empty,
  Table,
  Badge,
} from 'antd';
import { GridContent, PageHeaderWrapper, RouteContext } from '@ant-design/pro-layout';
import React, { FC, useEffect, useState } from 'react';
import { connect, Dispatch, FormToProcessState } from 'umi';
import styles from './style.less';
import DeclarationProcess from './component/DeclarationProcess';
import FinancialProcess from './component/FinancialProcess';
import ClaimProcess from './component/ClaimProcess';


const extra = (
  <div className={styles.moreInfo}>
    <Statistic title="状态" value="待审批" />
    <Statistic title="订单金额" value={568.08} prefix="¥" />
  </div>
);

const description = (
  <RouteContext.Consumer>
    {({ isMobile }) => (
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
    )}
  </RouteContext.Consumer>
);

interface FormToProcessProps {
  loading: boolean;
  myFormToProcess: FormToProcessState;
  dispatch: Dispatch
}

const FormToProcess: FC<FormToProcessProps> = (props) => {
  const { dispatch, myFormToProcess, loading } = props;
  useEffect(() => {
    dispatch({
      type: 'myFormToProcess/fetchMessage',
    });
  }, []);
  const [tabKey, setTabKey] = useState<string>('declaration');
  const tabList = [
    {
      key: 'declaration',
      tab:
        <Badge count={myFormToProcess.msg?.dform.filter(u=>u.state === 'InProcess').length} offset={[10, 0]}><p>申报</p></Badge>,
    },
    {
      key: 'financial',
      tab: <Badge count={myFormToProcess.msg?.fform.filter(u=>u.state === 'InProcess').length} offset={[10, 0]}><p>财务申请</p></Badge>
    },
    {
      key: 'claimform',
      tab: <Badge count={myFormToProcess.msg?.cform.filter(u=>u.state === 'InProcess').length} offset={[10, 0]}><p>领用</p></Badge>
    },
  ];

  const contentList = {
    declaration: <DeclarationProcess />,
    claimform: <ClaimProcess />,
    financial: <FinancialProcess />
  };

  return (
    <PageHeaderWrapper
      title="我的待办"
      className={styles.pageHeader}
      content={description}
      extraContent={extra}
    >
      <div className={styles.main}>
        <GridContent>
          <Card style={{ marginBottom: 24 }} bordered={false} tabList={tabList} onTabChange={setTabKey}>
            {contentList[tabKey]}
          </Card>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
}

export default connect(
  ({
    myFormToProcess,
    loading,
  }: {
    myFormToProcess: FormToProcessState;
    loading: {
      effects: { [key: string]: boolean };
    };
  }) => ({
    myFormToProcess,
    loading: loading.effects['myFormToProcess/fetchAdvanced'],
  }),
)(FormToProcess);
